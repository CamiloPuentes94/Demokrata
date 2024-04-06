using Demokrata.Data;
using Demokrata.Modelo;

namespace Demokrata.Services
{
    public class UsuariosServices : IUsuariosServices
    {

        private readonly aplicacionDbContext _db;

        public UsuariosServices(aplicacionDbContext db)
        {
            _db = db;
        }

        public bool CreateUsuario(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.Now;
            _db.Usuarios.Add(usuario);

            return Guardar();
        }

        public bool UpdateUsuario(int id, Usuario usuario)
        {
            var usuarioDB = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuarioDB == null)
            {
                return false;
            }

            // Actualizar solo los campos que se enviaron en el usuario
            usuarioDB.PrimerNombre = usuario.PrimerNombre ?? usuarioDB.PrimerNombre;
            usuarioDB.SegundoNombre = usuario.SegundoNombre ?? usuarioDB.SegundoNombre;
            usuarioDB.PrimerApellido = usuario.PrimerApellido ?? usuarioDB.PrimerApellido;
            usuarioDB.SegundoApellido = usuario.SegundoApellido ?? usuarioDB.SegundoApellido;
            usuarioDB.FechaNacimiento = usuario.FechaNacimiento != default(DateTime) ? usuario.FechaNacimiento : usuarioDB.FechaNacimiento;
            usuarioDB.Sueldo = usuario.Sueldo != 0 ? usuario.Sueldo : usuarioDB.Sueldo;

            usuarioDB.FechaModificacion = DateTime.Now;

            _db.Usuarios.Update(usuarioDB);
            return Guardar();
        }

        public bool DeleteUsuario(int id)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return false;
            }

            _db.Usuarios.Remove(usuario);
            return Guardar();
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuarios.OrderBy(u => u.Id).ToList();
        }

        public Usuario GetUsuario(int id)
        {
            return _db.Usuarios.FirstOrDefault(u => u.Id == id);
        }


        public (ICollection<Usuario>, int) BuscarUsuario(string nombre, int page, int pageSize)
        {
            IQueryable<Usuario> query = _db.Usuarios;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.PrimerNombre.Contains(nombre) || e.PrimerApellido.Contains(nombre));
            }

            var totalCount = query.Count();
            var usuarios = query.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return (usuarios, totalCount);
        }


        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}

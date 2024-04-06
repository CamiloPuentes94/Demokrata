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

        public bool UpdateUsuario(Usuario usuario)
        {
            usuario.FechaModificacion = DateTime.Now;
            _db.Usuarios.Update(usuario);

            return Guardar();
        }

        public bool DeleteUsuario(Usuario usuario)
        {
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


        public ICollection<Usuario> BuscarUsuario(string nombre)
        {
            IQueryable<Usuario> query = _db.Usuarios;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.PrimerNombre.Contains(nombre) || e.PrimerApellido.Contains(nombre));
            }
            return query.ToList();
        }


        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}

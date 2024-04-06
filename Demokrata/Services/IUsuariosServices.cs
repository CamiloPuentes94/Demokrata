using Demokrata.Modelo;

namespace Demokrata.Services
{
    public interface IUsuariosServices
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        bool CreateUsuario(Usuario usuario);
        bool UpdateUsuario(int id, Usuario usuario);
        bool DeleteUsuario(int id);
        ICollection<Usuario> BuscarUsuario(string nombre);
    }
}

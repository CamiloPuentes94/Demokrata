using Demokrata.Modelo;

namespace Demokrata.Services
{
    public interface IUsuariosServices
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        bool CreateUsuario(Usuario usuario);
        bool UpdateUsuario(Usuario usuario);
        bool DeleteUsuario(Usuario usuario);
        ICollection<Usuario> BuscarUsuario(string nombre);
    }
}

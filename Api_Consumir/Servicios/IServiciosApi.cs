using Api_Consumir.Models;

namespace Api_Consumir.NewFolder
{
    public interface IServiciosApi
    {
        Task<List<Producto>> Lista();
        Task<bool> Guardar(Producto producto);
        Task<Producto> Obtener(int idProducto);
        Task<bool> Borrar(int idProducto);
        Task<bool> Editar(Producto producto);

    }
}

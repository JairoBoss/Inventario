using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeInventarios.Clases
{
    class Producto
    {
        
        private string nombreDelProducto;
        private string categoria;
        private string marca;
        private int cantidad;
        private Double precio;
        private string descripcion;
        private string rutaDeLaImagen;
        private DateTime diaDeRegistro;

        public Producto(string nombreDelProducto, string categoria, string marca, int cantidad, Double precio, string descripcion, string rutaDeLaImagen, DateTime diaDeRegistro)
        {
            this.nombreDelProducto = nombreDelProducto;
            this.categoria = categoria;
            this.marca = marca;
            this.cantidad = cantidad;
            this.precio = precio;
            this.descripcion = descripcion;
            this.rutaDeLaImagen = rutaDeLaImagen;
            this.diaDeRegistro = diaDeRegistro;
        }

        public Producto(string nombreDelProducto, string categoria, string marca, int cantidad, Double precio, string descripcion, DateTime diaDeRegistro)
        {
            this.nombreDelProducto = nombreDelProducto;
            this.categoria = categoria;
            this.marca = marca;
            this.cantidad = cantidad;
            this.precio = precio;
            this.descripcion = descripcion;
            this.diaDeRegistro = diaDeRegistro;
        }

        public string NombreDelProducto { get => nombreDelProducto; set => nombreDelProducto = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Marca { get => marca; set => marca = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public Double Precio { get => precio; set => precio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string RutaDeLaImagen { get => rutaDeLaImagen; set => rutaDeLaImagen = value; }
        public DateTime DiaDeRegistro { get => diaDeRegistro; set => diaDeRegistro = value; }
    }
}

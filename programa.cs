using System;
using System.Collections.Generic;

namespace AgendaEstructurasDatos
{
    // Clase que representa el modelo de datos de cada contacto
    public class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Categoria { get; set; }

        public Contacto(string nombre, string telefono, string correo, string categoria)
        {
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"[Nombre: {Nombre} | Tel: {Telefono} | Correo: {Correo} | Categoria: {Categoria}]";
        }
    }

    class Program
    {
        // =========================================================================
        // IMPLEMENTACIÓN DE LAS 5 ESTRUCTURAS DE DATOS REQUERIDAS
        // =========================================================================

        // 1. Arreglo Unidimensional (Vector): Categorías fijas
        static string[] categorias = new string[] { "Familia", "Trabajo", "Amigos", "General" };

        // 2. Arreglo Bidimensional (Matriz): Contactos predeterminados (3 filas x 3 columnas)
        static string[,] matrizContactosBase = new string[,]
        {
            { "Carlos Lopez", "5551234567", "carlos@mail.com" },
            { "Ana Martinez", "5559876543", "ana@mail.com" },
            { "Beatriz Gomez", "5554567890", "beatriz@mail.com" }
        };

        // 3. Lista Dinámica (List<T>): Directorio principal de contactos activos
        static List<Contacto> directorio = new List<Contacto>();

        // 4. Pila (Stack<T> - LIFO): Historial de acciones
        static Stack<string> historialPila = new Stack<string>();

        // 5. Cola (Queue<T> - FIFO): Fila de llamadas o pendientes por atender
        static Queue<string> pendientesCola = new Queue<string>();

        static void Main(string[] args)
        {
            InicializarDesdeMatriz();

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("  SISTEMA DE AGENDA - UNIVERSIDAD DE LONDRES");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Agregar contacto (Lista Dinámica + Pila)");
                Console.WriteLine("2. Buscar contacto (Lista Dinámica)");
                Console.WriteLine("3. Eliminar contacto (Lista Dinámica + Pila)");
                Console.WriteLine("4. Mostrar agenda completa");
                Console.WriteLine("5. Ordenar contactos (Por Nombre o Teléfono)");
                Console.WriteLine("6. Agregar llamada pendiente (Cola FIFO)");
                Console.WriteLine("7. Atender siguiente pendiente (Cola FIFO)");
                Console.WriteLine("8. Ver historial de acciones (Pila LIFO)");
                Console.WriteLine("9. Salir");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": AgregarContacto(); break;
                    case "2": BuscarContacto(); break;
                    case "3": EliminarContacto(); break;
                    case "4": MostrarAgenda(); break;
                    case "5": OrdenarContactos(); break;
                    case "6": AgregarPendiente(); break;
                    case "7": AtenderPendiente(); break;
                    case "8": MostrarHistorial(); break;
                    case "9": salir = true; break;
                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Carga inicial desde la Matriz Bidimensional a la Lista Dinámica
        static void InicializarDesdeMatriz()
        {
            for (int i = 0; i < matrizContactosBase.GetLength(0); i++)
            {
                string nombre = matrizContactosBase[i, 0];
                string telefono = matrizContactosBase[i, 1];
                string correo = matrizContactosBase[i, 2];
                string categoria = categorias[i % categorias.Length]; // Asigna del Vector 1D

                directorio.Add(new Contacto(nombre, telefono, correo, categoria));
            }
            historialPila.Push("Sistema inicializado con datos base de la Matriz 2D.");
        }

        // 1. AGREGAR CONTACTO
        static void AgregarContacto()
        {
            Console.WriteLine("--- AGREGAR CONTACTO ---");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Teléfono: ");
            string tel = Console.ReadLine();
            Console.Write("Correo: ");
            string correo = Console.ReadLine();

            Console.WriteLine("\nSeleccione una Categoría (Vector 1D):");
            for (int i = 0; i < categorias.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categorias[i]}");
            }
            Console.Write("Opción: ");
            int idxSelec;
            int.TryParse(Console.ReadLine(), out idxSelec);
            string catSeleccionada = (idxSelec >= 1 && idxSelec <= categorias.Length) ? categorias[idxSelec - 1] : categorias[3];

            Contacto nuevo = new Contacto(nombre, tel, correo, catSeleccionada);
            directorio.Add(nuevo); // Inserta en Lista Dinámica

            string accion = $"Contacto agregado: {nombre}";
            historialPila.Push(accion); // Apila en Pila LIFO

            Console.WriteLine("\n¡Contacto agregado con éxito!");
            Pausar();
        }

        // 2. BUSCAR CONTACTO
        static void BuscarContacto()
        {
            Console.WriteLine("--- BUSCAR CONTACTO ---");
            Console.Write("Ingrese el nombre o teléfono a buscar: ");
            string criterio = Console.ReadLine().ToLower();

            bool encontrado = false;
            foreach (var c in directorio)
            {
                if (c.Nombre.ToLower().Contains(criterio) || c.Telefono.Contains(criterio))
                {
                    Console.WriteLine($"Encontrado -> {c}");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No se encontraron coincidencias.");
            }
            Pausar();
        }

        // 3. ELIMINAR CONTACTO
        static void EliminarContacto()
        {
            Console.WriteLine("--- ELIMINAR CONTACTO ---");
            Console.Write("Ingrese el nombre exacto del contacto a eliminar: ");
            string nombre = Console.ReadLine();

            Contacto aEliminar = directorio.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (aEliminar != null)
            {
                directorio.Remove(aEliminar); // Elimina de Lista Dinámica
                string accion = $"Contacto eliminado: {aEliminar.Nombre}";
                historialPila.Push(accion); // Registra en Pila LIFO

                Console.WriteLine($"¡Contacto '{nombre}' eliminado correctamente!");
            }
            else
            {
                Console.WriteLine("Contacto no encontrado.");
            }
            Pausar();
        }

        // 4. MOSTRAR AGENDA COMPLETA
        static void MostrarAgenda()
        {
            Console.WriteLine("--- AGENDA COMPLETA (Lista Dinámica) ---");
            if (directorio.Count == 0)
            {
                Console.WriteLine("La agenda está vacía.");
            }
            else
            {
                for (int i = 0; i < directorio.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {directorio[i]}");
                }
            }
            Pausar();
        }

        // 5. ORDENAR CONTACTOS
        static void OrdenarContactos()
        {
            Console.WriteLine("--- ORDENAR CONTACTOS ---");
            Console.WriteLine("1. Ordenar alfabéticamente por Nombre");
            Console.WriteLine("2. Ordenar numéricamente por Teléfono");
            Console.Write("Selección: ");
            string op = Console.ReadLine();

            if (op == "1")
            {
                directorio.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.OrdinalIgnoreCase));
                historialPila.Push("Directorio ordenado por Nombre.");
                Console.WriteLine("\n¡Contactos ordenados por Nombre exitosamente!");
            }
            else if (op == "2")
            {
                directorio.Sort((a, b) => string.Compare(a.Telefono, b.Telefono));
                historialPila.Push("Directorio ordenado por Teléfono.");
                Console.WriteLine("\n¡Contactos ordenados por Teléfono exitosamente!");
            }
            else
            {
                Console.WriteLine("\nOpción no válida.");
                Pausar();
                return;
            }

            // Muestra automáticamente los contactos ordenados
            Console.WriteLine("\n--- AGENDA ORDENADA ---");
            for (int i = 0; i < directorio.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {directorio[i]}");
            }

            Pausar();
        }

        // 6. AGREGAR PENDIENTE (COLA - FIFO)
        static void AgregarPendiente()
        {
            Console.WriteLine("--- REGISTRAR LLAMADA PENDIENTE (Cola) ---");
            Console.Write("Nombre o motivo de la llamada: ");
            string pendiente = Console.ReadLine();

            pendientesCola.Enqueue(pendiente); // Encola en FIFO
            historialPila.Push($"Pendiente encolado: {pendiente}");

            Console.WriteLine($"\nLlamada '{pendiente}' agregada a la cola.");
            Pausar();
        }

        // 7. ATENDER PENDIENTE (COLA - FIFO)
        static void AtenderPendiente()
        {
            Console.WriteLine("--- ATENDER SIGUIENTE PENDIENTE (Cola FIFO) ---");
            if (pendientesCola.Count > 0)
            {
                string atendido = pendientesCola.Dequeue(); // Desencola el primero que llegó
                historialPila.Push($"Pendiente atendido: {atendido}");
                Console.WriteLine($"Atendiendo ahora a: {atendido}");
            }
            else
            {
                Console.WriteLine("No hay llamadas pendientes en la cola.");
            }
            Pausar();
        }

        // 8. MOSTRAR HISTORIAL (PILA - LIFO)
        static void MostrarHistorial()
        {
            Console.WriteLine("--- HISTORIAL DE AUDITORÍA (Pila LIFO) ---");
            if (historialPila.Count == 0)
            {
                Console.WriteLine("El historial está vacío.");
            }
            else
            {
                foreach (var accion in historialPila)
                {
                    Console.WriteLine($"• {accion}");
                }
            }
            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
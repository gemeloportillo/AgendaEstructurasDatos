# 📇 Sistema de Agenda Inteligente - Proyecto Integrador

**Universidad de Londres**  
**Materia:** Estructura y Representación de Datos  
**Licenciatura en Informática**  
**Matrícula:** 168823  
**Nombre:** Miguel Angel Portillo Bobadilla 
**Fecha de Entrega:** 11 de Septiembre de 2026  

---

## 📌 Herramientas utilizadas

-GDB Online debugger  (para desarrollo y probar al vuelo)
-GitHub (para respaldar mi código y generar el ejecutable)
-Google Docs (para documentación)



## 📌 Descripción del Proyecto

Este proyecto consiste en un **Sistema de Agenda de Contactos Inteligente** desarrollado para la consola de comandos. Su propósito es aplicar de manera integrada y práctica las 5 estructuras de datos fundamentales de la asignatura (**Arreglo Unidimensional, Arreglo Bidimensional, Lista Dinámica, Pila y Cola**), ofreciendo una solución funcional para la administración de contactos, historial de actividad y gestión de pendientes.

---

## 🛠️ Arquitectura y Estructuras de Datos Utilizadas

Para cumplir con los parámetros de evaluación, el sistema organiza la información distribuyéndola de la siguiente forma:

| Estructura | Módulo en la Agenda | Justificación Técnica |
| :--- | :--- | :--- |
| **Arreglo Unidimensional (`Vector`)** | Lista de Categorías (`Familia`, `Trabajo`, `Amigos`, etc.) | Almacena elementos de tipo estático con tamaño fijo y acceso directo indexado $O(1)$. |
| **Arreglo Bidimensional (`Matriz`)** | Matriz de Contactos Básicos / Predeterminados | Representa información estructurada en formato tabular de filas (registros) y columnas (campos). |
| **Lista Dinámica (`Linked List`)** | Directorio Principal de Contactos | Permite realizar operaciones complejas (agregar, buscar, eliminar y ordenar) con tamaño dinámico en tiempo de ejecución. |
| **Pila (`Stack` - LIFO)** | Historial de Acciones / Auditoría | Mantiene el registro de eventos recientes para consultar o revertir la última acción realizada (*Last-In, First-Out*). |
| **Cola (`Queue` - FIFO)** | Turnos / Tareas y Llamadas Pendientes | Gestiona una fila de atención prioritaria respetando el orden secuencial de llegada (*First-In, First-Out*). |

---



## 🗺️ Mapa Conceptual de Organización de Datos

```text
                               ┌─────────────────────────────────────────────────┐
                               │           SISTEMA GENERAL DE AGENDA             │
                               └────────────────────────┬────────────────────────┘
                                                        │
          ┌─────────────────────┬───────────────────────┼───────────────────────┬─────────────────────┐
          │                     │                       │                       │                     │
          ▼                     ▼                       ▼                       ▼                     ▼
┌───────────────────┐ ┌───────────────────┐ ┌──────────────────────┐ ┌───────────────────┐ ┌───────────────────┐
│ 1. VECTOR (1D)    │ │ 2. MATRIZ (2D)    │ │ 3. LISTA DINÁMICA    │ │ 4. PILA (STACK)   │ │ 5. COLA (QUEUE)   │
│   [Categorías]    │ │  [Matriz Datos]   │ │ [Directorio Contacto]│ │    [Historial]    │ │   [Pendientes]    │
└─────────┬─────────┘ └─────────┬─────────┘ └───────────┬──────────┘ └─────────┬─────────┘ └─────────┬─────────┘
          │                     │                       │                       │                     │
          ▼                     ▼                       ▼                       ▼                     ▼
    [0] "Familia"       [Fila 0] Name,Tel...      (Nodo 1) -> (Nodo 2)    ┌───────────────┐     ┌──────────────┐
    [1] "Trabajo"       [Fila 1] Name,Tel...      - Agregar - Eliminar    │ ÚLTIMA ACCIÓN │ TOP │  PRIMER PEND.│ FRONT
    [2] "Amigos"        [Fila 2] Name,Tel...      - Buscar  - Ordenar     └───────────────┘     └──────────────┘




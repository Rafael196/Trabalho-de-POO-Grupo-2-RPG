using System;
using System.Collections.Generic;

namespace CampusQuest.Eventos;

public static class EventoBus
{
    private static readonly Dictionary<Type, List<Delegate>> Observadores = new();

    public static void Inscrever<TEvento>(Action<TEvento> handler) where TEvento : IEventoJogo
    {
        if (handler == null)
        {
            return;
        }

        Type tipo = typeof(TEvento);
        if (!Observadores.TryGetValue(tipo, out List<Delegate> lista))
        {
            lista = new List<Delegate>();
            Observadores[tipo] = lista;
        }

        lista.Add(handler);
    }

    public static void Remover<TEvento>(Action<TEvento> handler) where TEvento : IEventoJogo
    {
        if (handler == null)
        {
            return;
        }

        Type tipo = typeof(TEvento);
        if (!Observadores.TryGetValue(tipo, out List<Delegate> lista))
        {
            return;
        }

        lista.Remove(handler);
    }

    public static void Publicar<TEvento>(TEvento evento) where TEvento : IEventoJogo
    {
        if (evento == null)
        {
            return;
        }

        Type tipo = typeof(TEvento);
        if (!Observadores.TryGetValue(tipo, out List<Delegate> lista))
        {
            return;
        }

        foreach (Delegate handler in lista)
        {
            if (handler is Action<TEvento> action)
            {
                action(evento);
            }
        }
    }
}

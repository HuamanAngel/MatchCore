using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class InstanceCloner
{
    public static T Clonar<T>(T objeto)
    {
        // Se serializa el objeto a una secuencia de bytes
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, objeto);

        // Se mueve el puntero al inicio del stream
        stream.Seek(0, SeekOrigin.Begin);

        // Se deserializa el objeto desde la secuencia de bytes
        T objetoCopia = (T)formatter.Deserialize(stream);

        return objetoCopia;
    }
}
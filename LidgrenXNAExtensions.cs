﻿using System;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Game.Shared.Utility
{
    public static class LidgrenXNAExtensions
    {
        /// <summary>
        ///     Write a Point
        /// </summary>
        public static void Write(this NetBuffer message, Point value)
        {
            message.Write(value.X);
            message.Write(value.Y);
        }

        /// <summary>
        ///     Read a Point
        /// </summary>
        public static Point ReadPoint(this NetBuffer message)
        {
            return new Point(message.ReadInt32(), message.ReadInt32());
        }

        /// <summary>
        ///     Write a Single with half precision (16 bits)
        /// </summary>
        public static void WriteHalfPrecision(this NetBuffer message, float value)
        {
            message.Write(new HalfSingle(value).PackedValue);
        }

        /// <summary>
        ///     Reads a half precision Single written using WriteHalfPrecision(float)
        /// </summary>
        public static float ReadHalfPrecisionSingle(this NetBuffer message)
        {
            var h = new HalfSingle();
            h.PackedValue = message.ReadUInt16();
            return h.ToSingle();
        }

        /// <summary>
        ///     Writes a Vector2
        /// </summary>
        public static void Write(this NetBuffer message, Vector2 vector)
        {
            message.Write(vector.X);
            message.Write(vector.Y);
        }

        /// <summary>
        ///     Reads a Vector2
        /// </summary>
        public static Vector2 ReadVector2(this NetBuffer message)
        {
            Vector2 retval;
            retval.X = message.ReadSingle();
            retval.Y = message.ReadSingle();
            return retval;
        }

        /// <summary>
        ///     Writes a Vector3
        /// </summary>
        public static void Write(this NetBuffer message, Vector3 vector)
        {
            message.Write(vector.X);
            message.Write(vector.Y);
            message.Write(vector.Z);
        }

        /// <summary>
        ///     Writes a Vector3 at half precision
        /// </summary>
        public static void WriteHalfPrecision(this NetBuffer message, Vector3 vector)
        {
            message.Write(new HalfSingle(vector.X).PackedValue);
            message.Write(new HalfSingle(vector.Y).PackedValue);
            message.Write(new HalfSingle(vector.Z).PackedValue);
        }

        /// <summary>
        ///     Reads a Vector3
        /// </summary>
        public static Vector3 ReadVector3(this NetBuffer message)
        {
            Vector3 retval;
            retval.X = message.ReadSingle();
            retval.Y = message.ReadSingle();
            retval.Z = message.ReadSingle();
            return retval;
        }

        /// <summary>
        ///     Writes a Vector3 at half precision
        /// </summary>
        public static Vector3 ReadHalfPrecisionVector3(this NetBuffer message)
        {
            var hx = new HalfSingle();
            hx.PackedValue = message.ReadUInt16();

            var hy = new HalfSingle();
            hy.PackedValue = message.ReadUInt16();

            var hz = new HalfSingle();
            hz.PackedValue = message.ReadUInt16();

            Vector3 retval;
            retval.X = hx.ToSingle();
            retval.Y = hy.ToSingle();
            retval.Z = hz.ToSingle();
            return retval;
        }

        /// <summary>
        ///     Writes a Vector4
        /// </summary>
        public static void Write(this NetBuffer message, Vector4 vector)
        {
            message.Write(vector.X);
            message.Write(vector.Y);
            message.Write(vector.Z);
            message.Write(vector.W);
        }

        /// <summary>
        ///     Reads a Vector4
        /// </summary>
        public static Vector4 ReadVector4(this NetBuffer message)
        {
            Vector4 retval;
            retval.X = message.ReadSingle();
            retval.Y = message.ReadSingle();
            retval.Z = message.ReadSingle();
            retval.W = message.ReadSingle();
            return retval;
        }


        /// <summary>
        ///     Writes a unit vector (ie. a vector of length 1.0, for example a surface normal)
        ///     using specified number of bits
        /// </summary>
        public static void WriteUnitVector3(this NetBuffer message, Vector3 unitVector, int numberOfBits)
        {
            var x = unitVector.X;
            var y = unitVector.Y;
            var z = unitVector.Z;
            var invPi = 1.0 / Math.PI;
            var phi = (float) (Math.Atan2(x, y) * invPi);
            var theta = (float) (Math.Atan2(z, Math.Sqrt(x * x + y * y)) * (invPi * 2));

            var halfBits = numberOfBits / 2;
            message.WriteSignedSingle(phi, halfBits);
            message.WriteSignedSingle(theta, numberOfBits - halfBits);
        }

        /// <summary>
        ///     Reads a unit vector written using WriteUnitVector3(numberOfBits)
        /// </summary>
        public static Vector3 ReadUnitVector3(this NetBuffer message, int numberOfBits)
        {
            var halfBits = numberOfBits / 2;
            var phi = message.ReadSignedSingle(halfBits) * (float) Math.PI;
            var theta = message.ReadSignedSingle(numberOfBits - halfBits) * (float) (Math.PI * 0.5);

            Vector3 retval;
            retval.X = (float) (Math.Sin(phi) * Math.Cos(theta));
            retval.Y = (float) (Math.Cos(phi) * Math.Cos(theta));
            retval.Z = (float) Math.Sin(theta);

            return retval;
        }

        /// <summary>
        ///     Writes a unit quaternion using the specified number of bits per element
        ///     for a total of 4 x bitsPerElements bits. Suggested value is 8 to 24 bits.
        /// </summary>
        public static void WriteRotation(this NetBuffer message, Quaternion quaternion, int bitsPerElement)
        {
            if (quaternion.X > 1.0f)
                quaternion.X = 1.0f;
            if (quaternion.Y > 1.0f)
                quaternion.Y = 1.0f;
            if (quaternion.Z > 1.0f)
                quaternion.Z = 1.0f;
            if (quaternion.W > 1.0f)
                quaternion.W = 1.0f;
            if (quaternion.X < -1.0f)
                quaternion.X = -1.0f;
            if (quaternion.Y < -1.0f)
                quaternion.Y = -1.0f;
            if (quaternion.Z < -1.0f)
                quaternion.Z = -1.0f;
            if (quaternion.W < -1.0f)
                quaternion.W = -1.0f;

            message.WriteSignedSingle(quaternion.X, bitsPerElement);
            message.WriteSignedSingle(quaternion.Y, bitsPerElement);
            message.WriteSignedSingle(quaternion.Z, bitsPerElement);
            message.WriteSignedSingle(quaternion.W, bitsPerElement);
        }

        /// <summary>
        ///     Reads a unit quaternion written using WriteRotation(... ,bitsPerElement)
        /// </summary>
        public static Quaternion ReadRotation(this NetBuffer message, int bitsPerElement)
        {
            Quaternion retval;
            retval.X = message.ReadSignedSingle(bitsPerElement);
            retval.Y = message.ReadSignedSingle(bitsPerElement);
            retval.Z = message.ReadSignedSingle(bitsPerElement);
            retval.W = message.ReadSignedSingle(bitsPerElement);
            return retval;
        }

        /// <summary>
        ///     Writes an orthonormal matrix (rotation, translation but not scaling or projection)
        /// </summary>
        public static void WriteMatrix(this NetBuffer message, ref Matrix matrix)
        {
            var rot = Quaternion.CreateFromRotationMatrix(matrix);
            WriteRotation(message, rot, 24);
            message.Write(matrix.M41);
            message.Write(matrix.M42);
            message.Write(matrix.M43);
        }

        /// <summary>
        ///     Writes an orthonormal matrix (rotation, translation but no scaling or projection)
        /// </summary>
        public static void WriteMatrix(this NetBuffer message, Matrix matrix)
        {
            var rot = Quaternion.CreateFromRotationMatrix(matrix);
            WriteRotation(message, rot, 24);
            message.Write(matrix.M41);
            message.Write(matrix.M42);
            message.Write(matrix.M43);
        }

        /// <summary>
        ///     Reads a matrix written using WriteMatrix()
        /// </summary>
        public static Matrix ReadMatrix(this NetBuffer message)
        {
            var rot = ReadRotation(message, 24);
            var retval = Matrix.CreateFromQuaternion(rot);
            retval.M41 = message.ReadSingle();
            retval.M42 = message.ReadSingle();
            retval.M43 = message.ReadSingle();
            return retval;
        }

        /// <summary>
        ///     Reads a matrix written using WriteMatrix()
        /// </summary>
        public static void ReadMatrix(this NetBuffer message, ref Matrix destination)
        {
            var rot = ReadRotation(message, 24);
            destination = Matrix.CreateFromQuaternion(rot);
            destination.M41 = message.ReadSingle();
            destination.M42 = message.ReadSingle();
            destination.M43 = message.ReadSingle();
        }

        /// <summary>
        ///     Writes a bounding sphere
        /// </summary>
        public static void Write(this NetBuffer message, BoundingSphere bounds)
        {
            message.Write(bounds.Center.X);
            message.Write(bounds.Center.Y);
            message.Write(bounds.Center.Z);
            message.Write(bounds.Radius);
        }

        /// <summary>
        ///     Reads a bounding sphere written using Write(message, BoundingSphere)
        /// </summary>
        public static BoundingSphere ReadBoundingSphere(this NetBuffer message)
        {
            BoundingSphere retval;
            retval.Center.X = message.ReadSingle();
            retval.Center.Y = message.ReadSingle();
            retval.Center.Z = message.ReadSingle();
            retval.Radius = message.ReadSingle();
            return retval;
        }
    }
}
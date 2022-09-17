using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LasFiguras
{
    class Figura
    {
        private int shaderProgramHandle;
        private int vertexArrayHandle;
        private int vertexBufferHandle;

        public Figura(Punto p)
        {
            float[] vertices = {
                p.x - 0.3f, p.y - 0.3f, 0.0f, //Bottom-left vertex
                p.x + 0.3f, p.y - 0.3f, 0.0f, //Bottom-right vertex
                p.x + 0.0f, p.y + 0.3f, 0.0f  //Top vertex
            };

            vertexBufferHandle = GL.GenBuffer();//generate buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);

            string vertexShaderCode =
            @"
                #version 330 core

                layout (location = 0) in vec3 aPosition;

                void main(){
                    gl_Position = vec4(aPosition,1f);
                }
            ";

            string pixelShaderCode =
            @"
                #version 330 core

                out vec4 pixelColor;

                void main(){
                    pixelColor = vec4(0.0f, 1.0f, 1.0f, 1.0f);
                }
            ";

            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            int pixelShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(pixelShaderHandle, pixelShaderCode);
            GL.CompileShader(pixelShaderHandle);

            shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, pixelShaderHandle);

            GL.LinkProgram(shaderProgramHandle);

            GL.DetachShader(shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(shaderProgramHandle, pixelShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(pixelShaderHandle);
        }
        public void dibujar()
        {
            GL.UseProgram(shaderProgramHandle);
            GL.BindVertexArray(vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
        public void delete()
        {

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(shaderProgramHandle);
        }
    }
}

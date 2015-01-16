//
//  Author:
//    Paál Gyula paalgyula@gmail.com
//
//  Copyright (c) 2015, GW-Systems Kft. All Rights Reserved
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using System.Globalization;

namespace Unturned
{
    public class HttpRequest
    {
        public static readonly String POST = "POST";
        public static readonly String GET = "GET";
        public static readonly String HEAD = "HEAD";
        public static readonly String PUT = "PUT";

        private Dictionary<string, List<string>> headers = new Dictionary<string, List<string>> ();

        public static readonly byte[] EOL = { (byte)'\r', (byte)'\n' };

        public byte[] bytes;

        public String UserAgent { get; set; }
        public string ContentType { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }

        private TcpClient m_client;
        private Uri m_uri;

        public HttpRequest(String url)
        {
            this.m_client = new TcpClient ();
            this.m_uri = new Uri(url);
            this.UserAgent = "Unturned";
            this.ContentType = "application/xml";
            this.Protocol = "HTTP/1.1";
        }

        public TextReader DoGet() {
            this.m_client.Connect(m_uri.Host, m_uri.Port);
            this.m_client.SendTimeout = 5;
            this.Method = GET;
            SetHeader ("Host", m_uri.Host);

            NetworkStream ns = m_client.GetStream();
            WriteToStream(ns);

            return ReadHeader(ns);
        }

        private StreamReader ReadHeader(NetworkStream ns)
        {
            StreamReader reader = new StreamReader(ns);
            String statusLine = reader.ReadLine();
            Console.WriteLine("Response status: " + statusLine.Split(' ')[1] );
            while (true)
            {
                String line = reader.ReadLine();
                if (line != String.Empty)
                {
                    Console.WriteLine("Header: " + line);
                }
                else
                {
                    return reader;
                }
            }
        }

        void WriteToStream (Stream outputStream)
        {
            var stream = new BinaryWriter (outputStream);
            stream.Write (ASCIIEncoding.ASCII.GetBytes (Method.ToUpper() + " " + m_uri.PathAndQuery + " " + Protocol));
            stream.Write (EOL);
            foreach (string name in headers.Keys) {
                foreach (string value in headers[name]) {
                    stream.Write (ASCIIEncoding.ASCII.GetBytes (name));
                    stream.Write (':');
                    stream.Write (ASCIIEncoding.ASCII.GetBytes (value));
                    stream.Write (EOL);
                }
            }

            if (bytes != null && bytes.Length > 0) {
                if (GetHeader ("Content-Length") == "") {
                    stream.Write (ASCIIEncoding.ASCII.GetBytes ("content-length: " + bytes.Length.ToString ()));
                    stream.Write (EOL);
                    stream.Write (EOL);
                }
                stream.Write (bytes);
            } else {
                stream.Write (EOL);
            }
        }

        public string GetHeader (string name)
        {
            name = name.ToLower ().Trim ();
            if (!headers.ContainsKey (name))
                return "";
            return headers[name][0];
        }

        public void SetHeader (string name, string value)
        {
            name = name.ToLower ().Trim ();
            value = value.Trim ();
            if (!headers.ContainsKey (name))
                headers[name] = new List<string> ();
            headers[name].Clear ();
            headers[name].Add (value);
        }
    }
}


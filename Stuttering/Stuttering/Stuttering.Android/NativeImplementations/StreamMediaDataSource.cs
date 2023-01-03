using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stuttering.Droid.NativeImplementations
{
    public class StreamMediaDataSource : MediaDataSource
    {
        System.IO.Stream data;

        public StreamMediaDataSource(System.IO.Stream Data)
        {
            data = Data;
        }

        public override long Size
        {
            get
            {
                return data.Length;
            }
        }

        public override int ReadAt(long position, byte[] buffer, int offset, int size)
        {
            data.Seek(position, System.IO.SeekOrigin.Begin);
            return data.Read(buffer, offset, size);
        }

        public override void Close()
        {
            if (data != null)
            {
                data.Dispose();
                data = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (data != null)
            {
                data.Dispose();
                data = null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stuttering.Helper
{
    public interface IAudio
    {
        void Play(string url);
        void Stop();
        void Pause();
    }
}

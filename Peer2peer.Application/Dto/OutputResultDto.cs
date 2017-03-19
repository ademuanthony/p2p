using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Dto
{
    public class OutputResultDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class OutputResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

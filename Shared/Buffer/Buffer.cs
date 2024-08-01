using System.Threading.Tasks.Dataflow;

namespace Shared.Buffer
{

    public class Buffer
    {
        private int size;
        private int readPointer;
        private byte[] memory;

        public Buffer(int size){
            this.size = size;
            this.readPointer = 0;
            this.memory = new byte[size];
        }

        public void Write(int data){

        }

        public void Write(string data){

        }

        public void Read(){
            
        }
    }
}
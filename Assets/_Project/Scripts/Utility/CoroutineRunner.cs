using System.Collections;

namespace Utility
{
    public class CoroutineRunner : Singleton<CoroutineRunner>
    {
        public void RunCoroutine(IEnumerator coroutine) => 
            StartCoroutine(coroutine);
    }
}

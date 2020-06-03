

namespace Muc.Types.Extensions {

  using System.Collections;
  using System.Collections.Generic;

  public static class EnumeratorExtensions {
    public static IEnumerable<T> Enumerate<T>(this IEnumerator<T> topology) {
      while (topology.MoveNext())
        yield return topology.Current;
    }
  }
}
using System.Collections;

namespace Maze_Play
{
    public class Units : IEnumerable
    {
        private List<Unit> _units = new();

        public void Add(Unit unit)
        {
            _units.Add(unit);
        }

        public void Remove(Unit unit)
        {
            _units.Remove(unit);
        }

        public IEnumerator GetEnumerator()
        {
            return new UnitsEnumerator(_units);
        }
    }
}

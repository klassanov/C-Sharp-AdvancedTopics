namespace AdvancedTopics.LocalInversionOfControl
{
    internal class LocalIoCDemo
    {
        public static void Demo()
        {
            ListsDemo();
        }

        public static void ListsDemo()
        {
            var list = new List<int>();
            list.Add(25);

            var list2 = new List<int>();

            //Instead, we can do the following
            //Now the value that is being added is the one that controls
            25.AddTo(list, list2);
        }

        public static void ProcessCommand(string opcode)
        {
            //Not readable
            if (opcode == "AND" || opcode == "OR" || opcode == "XOR")
            {
            }

            //Refactor: way 1
            if (new[] { "AND", "OR", "XOR" }.Contains(opcode))
            {
            }

            //Refacotr: way 2
            if (opcode.IsOneOf("AND", "OR", "XOR"))
            {
            }
        }

        public static void Process(Person person)
        {
            //Unreadable
            if (person.Names.Count == 0) { }

            //Slightly better
            if (!person.Names.Any()) { }

            //Best
            if (person.HasNo(p => p.Names)) { }

            if (person.HasSome(p => p.Children)) { }

            if (person.BMHasNo(p => p.Children).And.Self.BMHasNo(p => p.Names))
            { }
        }
    }

    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();


    }

    public static class ExtensionMethods
    {
        

        //With fluent interface
        public static T AddTo<T>(this T self, params ICollection<T>[] lists)
        {
            foreach (var list in lists)
            {
                list.Add(self);
            }
            return self;
        }

        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static bool HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

        public static bool HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return props(self).Any();
        }


        public static BoolMarker<TSubject> BMHasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<T> HasNo<T,U>(this BoolMarker<T> marker, Func<T, IEnumerable<U>> props)
        {
            if(marker.PendingOp == BoolMarker<T>.Operation.And && !marker.Result)
            {
                return marker;
            }
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }
    }

    public struct BoolMarker<T>
    {
        public bool Result;
        public T Self;

        public enum Operation
        {
            None,
            And,
            Or
        };

        internal Operation PendingOp;

        internal BoolMarker(bool result, T self, BoolMarker<T>.Operation pendingOp)
        {
            Result = result;
            Self = self;
            PendingOp = pendingOp;
        }

        public BoolMarker(bool result, T self)
            : this(result, self, Operation.None)
        {
            Result = result;
            Self = self;
        }

        public static implicit operator bool(BoolMarker<T> self)
        {
            return self.Result;
        }

        public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);
    }
}

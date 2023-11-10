        //DUCK TYPING
        //----------------------------------------
        /* Duck typing allows an object to be passed in to a method that expects a certain type even
         * if it doesn’t inherit from that type. All it has to do is support the methods and properties
         * of the expected type in use by the method
         * 
         * Even though C# is mostly strongly typed language (leaving aside the new keyword dynamic) 
         * there are some parts where the .NET uses duck typing instead of the
         * traditional static strong type checking.
         * 
         * The compiler does not require class which is implementing these interfaces.
         * The compiler is satisfied by the type which has only a GetEnumerator method
         * 
         * The term duck typing is popularly explained by the phrase
         * "If it walks like a duck and quacks like a duck, it must be a duck"
         * 
         * Interestingly enough, certain features of C# already use duck typing
         * 
         * 
         * EXAMPLE 1:
         * -------------
         * The foreach keyword can also be used in a situation where your object does not implement IEnumerable or IEnumberable<T> interface
         * For example, to allow an object to be enumerated via the C# foreach operator, the object only needs to implement a set of methods
         * GetEnumerator() that takes no parameters and returns a type 
         *      - The returned type should have two members: 
         *          - A method MoveNext that takes no parameters and returns a boolean
         *          - A property Current with a getter that returns an Object
         * 
         * If .NET finds GetEnumerator() in a class, it can do for each on it, even if the class does not implement IEnumerable
         * 
         * 
         * EXAMPLE 2:
         * --------------
         * Dispose(), to use using(...), it is enough to implement the Dispose method, even if U don't implement the IDisposable interface
         *
         */

        //MIXINGS
        //-----------------------------------
        /* Mixins are a language concept that allows a programmer to inject some code into a class
         * Units of functionality are created in a class and then mixed in with other classes but which cannot itself be instantiated
         * We will use a combination of interfaces, extension methods and DUCK TYPING to implement this
         * A mixin class acts as the parent class, containing the desired functionality
         * Useful in object oriented languages which only support single inheritance like C#
         */
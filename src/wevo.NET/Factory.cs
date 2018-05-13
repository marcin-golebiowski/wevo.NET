namespace wevo.NET
{
    /**
    * An interface for generic factory, used for example in
    * {@link AddHardcodedIndividual} operator.
    * 
    * @param <T> type of objects the factory provides.
    */
    public interface Factory<T>
    {
        /**
         * Creates an object of type t.
         * @return Some object.
         */
        T Get();
    }
}

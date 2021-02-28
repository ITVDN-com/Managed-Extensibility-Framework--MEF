using System;
using System.Globalization;
using SDKInterface;

namespace SDK_Plugin
{
    [Serializable]
    public class AddInA : IAddIn
    {
        public AddInA()
        {
        }
        public String DoSomething(Int32 x)
        {
            return "AddIn_A: " + x.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class AddInB : MarshalByRefObject, IAddIn
    {
        public AddInB()
        {
        }
        public String DoSomething(Int32 x)
        {

            return "AddIn_B: " + (x * 2).ToString(CultureInfo.InvariantCulture);
        }
    }

    public class AddInC : MarshalByRefObject, IAddIn
    {
        public AddInC()
        {
        }
        public String DoSomething(Int32 x)
        {

            return "AddIn_C: " + (x * 2).ToString(CultureInfo.InvariantCulture);
        }
    }
}

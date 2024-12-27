//索引器定义与属性大同小异
using System.Reflection;
namespace Indexer
{   public class IndexerTest
    {
        int value1;
        int value2;
        //结合反射，通过字符串访问成员变量
        public int this[string name]
        {
            //value即为实例对象[xx]=value的value值
            set {
                Type type = typeof(IndexerTest);
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                FieldInfo info = type.GetField(name, flags);
                if (info != null)
                {
                    info.SetValue(this, value);
                }
                else
                    throw new InvalidOperationException();
            }
            get
            {
                Type type = typeof(IndexerTest);
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                FieldInfo info = type.GetField(name, flags);
                if (info != null)
                {
                    return (int)info.GetValue(this);
                }
                else
                    throw new InvalidOperationException();
            }
        }
        //索引器允许重载
        public int this[int index]
        {
            get { 
                if(index==1)
                    return value1;
                return value2;
            }
            set
            {
                if (index == 1)
                    value1 = value;
                else
                    value2 = value;
            }
        }
        public IndexerTest(int value1, int value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IndexerTest test = new IndexerTest(1, 2);
            Console.WriteLine(test["value1"]);
            test["value2"] = -1;
            Console.WriteLine(test[2]);
        }
    }
}
    


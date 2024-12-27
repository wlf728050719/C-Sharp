using System.Reflection;
namespace Reflect
{
    public class People
    {
        string name;
        int age;
        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public void say(string words)
        {
            Console.WriteLine(words);
        }
        public void say(int number)
        {
            Console.WriteLine(number);
        }
        public void sayHello()
        {
            Console.WriteLine("Hello");
        }
        public int compute(int a,int b)
        {
        return a + b; 
        }
        //将T类型转为U类型并输出
        public U ChangeType<T,U>(T input)
        {
            Console.WriteLine("input "+typeof(T).Name+" output "+typeof(U).Name);
            return (U)Convert.ChangeType(input, typeof(U));
        }
        public People(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public override string ToString()
        {
            return this.name+" "+this.age;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //使用反射
                //1.获取Type 类似java.class
                //获取方式1.typeof(类型) 2.Type.getType(变量) 3.变量.getType("全类名") 4.
                People people = new People("xyw", 22);
                Type type = typeof(People);
                type = people.GetType();
                type = Type.GetType("Reflect.People");
                //输出type名
                Console.WriteLine(type.Name);


                //2.声明获取的成员类型种类
                BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;


                //3.拿到Info以及使用Info前需要在第一个参数指定调用对象
                //GetField获取字段,"name"表示字段名
                FieldInfo fieldInfo = type.GetField("name", flag);
                Console.WriteLine(fieldInfo.GetValue(people));

                //GetProperty获取属性，"Name"表示属性名
                PropertyInfo propertyInfo = type.GetProperty("Name", flag);
                Console.WriteLine(propertyInfo.GetValue(people));

                //GetMethod获取方法
                MethodInfo methodInfo1 = type.GetMethod("sayHello", flag);
                //如果方法重载需要额外指定形参类型
                MethodInfo methodInfo2 = type.GetMethod("say", flag,new Type[] {typeof(string)});
                MethodInfo methodInfo3 = type.GetMethod("say",flag,new Type[] {typeof(int)});
                MethodInfo methodInfo4 = type.GetMethod("compute",flag);
                
                //方法无参调用
                methodInfo1.Invoke(people, null);
                //方法有参调用
                methodInfo2.Invoke(people, new object[]{"goodbye"});
                methodInfo3.Invoke(people, new object[] {1});
                //方法有返回值
                int result = (int)methodInfo4.Invoke(people, new object[] { 1, 1 });
                Console.WriteLine(result);
                //泛型方法
                int a = people.ChangeType<string,int>("1");//一般调用
                //获取时需要指定泛型类型
                MethodInfo methodInfo5 = type.GetMethod("ChangeType", flag).MakeGenericMethod(new Type[] {typeof(int),typeof(string)});
                string b = (string)methodInfo5.Invoke(people, new object[] { 1 });

                //GetConstructor获取构造函数
                ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(string),typeof(int)});
                People p1 = (People)constructorInfo.Invoke(new object[] { "lfc", 22 });
                Console.WriteLine(p1);
                //或用封装好的函数
                People p2 = (People)Activator.CreateInstance(typeof(People),"mmc",22);
                Console.WriteLine(p2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

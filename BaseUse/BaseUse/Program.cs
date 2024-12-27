//预处理指令
#define DEBUG
//System一般都引入，在当前命名空间使用其他空间方法和类时如果不引入需要用XX.XX调用，如System.Console(),（System好像默认引入，加不加无所谓）
using System;
//如果在单文件中没有声明命名空间，默认有隐藏的命名空间和类，所有语句均位于该类的Main方法中
//在没有命名空间的情况下，可以直接使用顶级语句，不用封装在类或命名空间中
//顶级语句包括变量声明，函数调用，控制流,异常处理，using引入命名空间
//顶级语句不包括类、结构、接口、枚举、委托的定义，定义隐藏类的构造函数和属性,(不能在方法里定义这些，自然不能在隐藏类的Main方法中使用这些.)
//Console.WriteLine("123");
#region  
namespace Program
{
    //结构定义 非顶级语句
    struct Test
    {
        public int a;
        public int b;
        public Test(int a,int b) {
            this.a = a; this.b = b;
        }
    }
    //接口以大写字母I开头
    public interface IMyInterfaceTest {
       public void test();
    }
    //必须实现接口方法
    public class Test1:IMyInterfaceTest
    {
        public int a;
        public int b;
        public Test1(int a,int b){
            this.a=a; this.b=b;
        }
        public void test()
        {
            Console.WriteLine("test");  
        }
        public virtual void vtest()
        {
            Console.WriteLine("vtest from test1");
        }
        //重载运算符
        public static int operator+(Test1 test1 ,Test1 test2)
        {
            return test1.a+test2.a;
        }
    }
    //成员初始化顺序，先基类构造函数、再static，再实例对象（同java)
    public class Test2:Test1
    {
        public int c;
        public int d;
        //必须先初始化基类
        public Test2(int c, int d):base(-1,-1)
        {
          this.c=c; this.d=d;
        }
        //可以有多个参数不同的构造函数，区分arkts
        public Test2(int a,int b,int c,int d):base(a,b)
        {
            //通过base使用基类属性
            base.test();
            this.c=c; this.d=d;
        }
        public override void vtest()
        {
            Console.WriteLine("vtest from test2");
        }
        //析构函数
        ~Test2()
        {
            Console.WriteLine("destory");
        }
        //一个文件/项目只能
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine("123");
        //}
    }
    //含有抽象方法必须使用abstract
    public abstract class Test3
    {
        //抽象类可以有成员属性
        private int a;
        //简单的访问器，类似java getter,setter,相当于有隐藏字段b，自动实现的访问器必须有get和set
        public int B { get; set; }
        //可定义抽象访问器，子类必须实现,本质是一个伪装成变量的特殊方法
        public abstract int C { get; set; }
        //自定义访问器
        public int A
        {
            get { return a; }
            //value可以看作set(value)
            set { a = value; Console.WriteLine("set:"+value); }
        }
        //当virtual方法没有方法体，退化为abstract
        public abstract void test();
        //抽象类可以有构造函数
        public Test3(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
    public class Test4:Test3
    {
        private int d;
        //访问时将D当作一个属性用即可
        public int D {
            get {
                return d;
            } 
            set
            {
                d = value;
            }
        }
        public override int C {  get; set; }
        //重新方法
        public override void test()
        {
            Console.WriteLine("test");
        }
       
        public Test4(int a,int b,int c,int d):base(a,b,c){
            D = d;
        }
    }
    //枚举定义 非顶级语句 区分java不能实现接口
    enum Days {Sun,Mon,Tue,Wed,Thu,Fri,Sat}
    public class Entrance {
        public static void Main(string[] args)
        {
            #if DEBUG
            Console.WriteLine("DEBUG");
            #elif RELEASE
            Console.WriteLine("RELEASE");
            #endif
            //基本类型
            //C#为强类型语言，类型间转化需要明确操作,下面会将' '转为ascall码32而非1转为字符串
            //但如果为""字符串则会输出1
            Console.WriteLine(1 + ' ');
            Console.WriteLine(1 + " ");
            //数组
            //声明：Int[] array;
            //初始化：array = new int[length](默认初始化为0)
            //声明时赋值:
            //int[] array = { 1, 2, 3 }
            //int[] array = new int[] { 1, 2, 3 }
            //int[] array = new int[5] { 1, 2, 3 }(错误，必须长度一致)
            //array1 = array2(指向同一位置内存)
            int[] array1 = new int[5] { 1, 2, 3, 4, 5 };
            int length = array1.Length;
            int[] array2 = new int[length];
            array2 = array1;
            array1[0] = -1;
            Console.WriteLine(array2[0]);
            //foreach遍历
            //foreach(type item in list)
            foreach (int i in array2)
                Console.Write(i.ToString() + " ");
            Console.WriteLine();
            //字符串，类java
            //new string(char[])构造string
            //string str=""赋值
            //允许字符串通过+拼接
            char[] array3 = new char[5] { 'h', 'e', 'l', 'l', 'o' };
            string hello = new string(array3);
            Console.WriteLine(hello);
            string hi = "hi";
            Console.WriteLine(hi);
            string hihi = hi + "hi";
            Console.WriteLine(hihi);
            //struct null为引用类型默认值，struct不能为null,在栈上，为类成员时在堆上
            //可定义构造函数，不能定义析构函数，无参构造函数自带（不能额外定义）,不能继承，可实现接口，成员不能abstract,virtual,或protected
            //可new调用构造函数创建
            Test test1;
            test1.a = 1;
            Console.WriteLine(test1.a);
            //Console.WriteLine(test.b);未赋值b不能使用
            //struct是值类型，复制整个结构内容而非引用
            Test test2= new Test(2,2);
            Test test3 = test2;
            test2.a = 1;
            Console.WriteLine(test3.a);
            //枚举自动调用ToString,输出枚举名
            Console.WriteLine(Days.Mon);
            //枚举可以类型强转为int,值为其序号,从0开始
            Console.WriteLine((int)Days.Mon);
            //C#多态：
            //静态：
            //函数/运算符重载
            //动态：
            //1.虚方法virtual父+override子 子类重写父方法，类似接口调用,如果基类虚方法为空方法体即可用abstract替换,若替换则类变为抽象类
            //抽象类可以有成员变量，成员方法以及构造函数，但不能直接使用构造函数实例化，必须被继承，相反的关键字是sealed密封类，不能被继承
            Test1 test4 = new  Test2(0, 0);
            Test1 test5 = new Test2(1, 1);
            test4.vtest();
            Console.WriteLine(test5 + test4);
            try
            {
                int b = 0;
                int a = 5 /b;
            }catch(Exception e){
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("end");
            }
            Test4 test6 = new Test4(1, 2, 3, 4);
            test6.D = 5;
            Console.Write(test6.D);
        }
    }
}
#endregion  
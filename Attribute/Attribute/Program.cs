using System.Reflection;
//C#特性，类似java注解
namespace Attribute
{
    //表明特性可添加到所有类型上，允许添加多个该特性
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    //如果自定义特性结尾为Attribute时使用时可以只写前面部分
    public class StateAttribute : System.Attribute
    {
        public int Hp { get; set; }
        public int Attack { get; set; }
        public StateAttribute(int hp, int attack)
        {
            Hp = hp;
            Attack = attack;
        }
    }
    //定义角色抽象类
    public abstract class Character
    {
        public int HP { get; set; }
        public int Attack { get; set; }
        public Character(Type type)
        {
           StateAttribute attribute=type.GetCustomAttribute<StateAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"StateAttribute is not defined on the type {type.Name}.");
            this.HP = attribute.Hp; 
            this.Attack = attribute.Attack;  
        }
        public abstract void hit(Character c);
        public void showPresentState()
        {
            Console.WriteLine($"HP: {this.HP}");  
        }
        
    }

    //通过特性代替配置文件初始化玩家
    [State(100,5)]
    public class Player : Character
    {
        public String Name { get; set; }
        public override void hit(Character c)
        {
            Console.WriteLine($"玩家攻击了伤害{this.Attack}");
            c.HP -= this.Attack;
        }
        public Player(string name):base(typeof(Player)) { this.Name = name; }
    }

    //通过特性代替配置文件初始化怪物
    [State(10,2)]
    public class Enemy : Character
    {
        public override void hit(Character c)
        {
            Console.WriteLine($"怪物攻击了伤害{this.Attack}");
            c.HP -= this.Attack;
        }
        public Enemy() : base(typeof(Enemy)) { }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("xyw");
            Enemy enemy1 = new Enemy();
            player.showPresentState();
            enemy1.showPresentState();
            player.hit(enemy1);
            enemy1.showPresentState();
        }
    }
}

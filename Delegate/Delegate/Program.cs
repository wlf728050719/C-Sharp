//委托允许类将类方法视为变量管理,可以实现策略模式
namespace Delegate
{
    //声明委托类型
    public delegate void Hit(Character attacker,Character attacked);
    //定义函数列表
    public class HitList
    {
        public static void NormalHit(Character attacker, Character attacked) {
            attacked.hp -= attacker.attack;
            Console.WriteLine("普通攻击");
        }
        public static void FireHit(Character attacker, Character attacked)
        {
            Console.WriteLine("火焰攻击");
            attacked.hp -= (attacker.attack+10);
        }
        public static void IceHit(Character attacker, Character attacked)
        {
            Console.WriteLine("寒冰攻击");
            attacked.hp -= (attacker.attack + 5);
        }
    }
    public class Character
    {
        public string name;
        public int attack;
        public int hp;
        //将函数看为变量使用
        public Hit hit;
        public Character(string name,int attack, int hp)
        {
            this.name = name;
            this.attack = attack;
            this.hp = hp;
            this.hit = HitList.NormalHit;
        }
        public void changeHit(Hit hit)
        {
            this.hit = new Hit(hit);
        }
        //相同类型的委托可以叠加
        public void AddHit(Hit hit)
        {
            this.hit += hit;
        }
        public void showInfo()
        {
            Console.WriteLine($"{name} attack: {attack} hp:{hp}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Character c1 = new Character("玩家", 10, 100);
            Character c2 = new Character("怪物",5,100);
            c1.hit(c1, c2);
            c2.showInfo();
            c1.changeHit(HitList.FireHit);
            c1.hit(c1, c2);
            c2.showInfo();
            c1.AddHit(HitList.IceHit);
            c1.hit(c1, c2);
            c2.showInfo();

        }
    }
}

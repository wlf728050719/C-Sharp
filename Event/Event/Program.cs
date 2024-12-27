namespace Event
{
    public delegate void EventHandler(object sender, EventArgs e);
    public class Buff
    {
        public string name;
        public int start;
        public int lasting;
        public int type;//0表示hp，1表示attack
        public int value;//buff数值
        public event EventHandler AddToPlayer;
        public event EventHandler RemoveFromPlayer;
        public Buff(string name, int start, int lasting,int type, int value)
        {
            this.name = name;
            this.start = start;
            this.lasting = lasting;
            this.type = type;
            this.AddToPlayer += Player.instance.addBuff;
            this.RemoveFromPlayer += Player.instance.removeBuff;
            this.value = value;
        }
        public void Active()
        {
            Console.WriteLine($"{name}开始生效，持续{lasting}秒");
            AddToPlayer?.Invoke(this, EventArgs.Empty);
        }
        public void Passive()
        {
            Console.WriteLine($"{name}结束生效");
            RemoveFromPlayer?.Invoke(this, EventArgs.Empty);
        }
    }
    public class Player
    {
        public int hp;
        public int attack;
        public static Player instance=new Player();
        public Player()
        {
            hp = 100;
            attack = 10;
        }
        public void addBuff(object sender,EventArgs e)
        {
            Buff buff=(Buff)sender;
            if (buff.type == 0)
                this.hp+=buff.value;
            else
                this.attack+=buff.value;
        }
        public void removeBuff(object sender,EventArgs e)
        {
            Buff buff=(Buff)sender;
            if (buff.type == 0)
                this.hp -= buff.value;
            else
                this.attack -= buff.value;
        }
        public void showState()
        {
            Console.WriteLine($"HP:{hp} ATTACK:{attack}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Buff> buffs = new List<Buff>();
            for (int i = 0; i <100; i++)
            {
                if (i == 2)
                {
                    Buff buff = new Buff("攻击力增加10", i, 5, 1, 10);
                    buff.Active();
                    buffs.Add(buff);
                }
                Thread.Sleep(1000);
                for(int j=0;j<buffs.Count;j++)
                {
                    Buff buff = buffs[j];
                    if(buff.start+buff.lasting<=i)
                    {
                        buff.Passive();
                        buffs.Remove(buff);
                        j--;
                    }
                }
                Player.instance.showState();
            }
        }
    }
}

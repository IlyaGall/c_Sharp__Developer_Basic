using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork5
{
    interface IFlyingRobot : IRobot
    {
        string GetRobotType() { return "I am a flying robot."; }
    }
}

using GameProtocol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void CallBack();
public delegate void Refresh(SelectRoomDTO value);
public delegate void SelectHero(int id);
   public class SelectEventUtil
    {
       public static CallBack selected;
       public static Refresh refreshView;
       public static SelectHero selectHero;
    }


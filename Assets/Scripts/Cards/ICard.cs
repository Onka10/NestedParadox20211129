interface ICard
{
    //召喚条件が無いカードもあるから後々変更するかも

    //召喚前の条件確認
    bool CheckTrigger();
}
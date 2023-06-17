using System;

[Serializable]
public class BuildData
{
    public BuildNameEnum nameEnum;
    public int state;

    public BuildData (BuildNameEnum name, int state)
    {
        this.nameEnum = name;
        this.state = state;

    }
}

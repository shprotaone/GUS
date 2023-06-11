using System;

[Serializable]
public class BuildData
{
    public BuildNameEnum nameEnum;
    public BuildStateEnum state;

    public BuildData (BuildNameEnum name, BuildStateEnum state)
    {
        this.nameEnum = name;
        this.state = state;

    }
}

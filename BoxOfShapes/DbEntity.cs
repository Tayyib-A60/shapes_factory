using System;
namespace BoxOfShapes
{
    public abstract class DbEntity
    {
        
        public  ObjectState State;
        public void Save()
        {
            if(State == ObjectState.New)
            {
                this.Insert();
            }
            else if(State == ObjectState.Changed)
            {
                this.Update();
             
            }
            else if(State == ObjectState.Removed)
            {
                this.Delete();
            }

        }

    public void ChangeState()
    {
        if(State == ObjectState.Unchanged)
            State = ObjectState.Changed;
            
    }

        internal abstract void Insert();
        internal abstract void Update();
        internal abstract void Delete();
    }
}

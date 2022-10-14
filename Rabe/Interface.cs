namespace Rabe;

public abstract class NativeObject:IDisposable
{
    public IntPtr Handle { get; private set; }
    public NativeObject(IntPtr handle)
    {
        Handle = handle;
    }

    protected abstract void FreeHandle(IntPtr handle);
    
    //This is the public method, it will HOPEFULLY but
    //not always be called by users of the class
    public void Dispose()
    {
        if (Handle != IntPtr.Zero)
        {
            FreeHandle(Handle);
            //preserve the handle in case of double dispose when gc call finalizer
            Handle = IntPtr.Zero;
            //clear up any unmanaged resources - this is safe to
            //put outside the disposing check because if the user
            //called dispose we want to also clean up unmanaged
            //resources, if the GC called Dispose then we only
            //want to clean up managed resources
        }

        //Now we have disposed of all our resources, the GC does not
        //need to do anything, stop the finalizer being called
        GC.SuppressFinalize(this);
    }

    //Finalize method for the object, will call Dispose for us
    //to clean up the resources if the user has not called it
    ~NativeObject()
    {
        //indicate this was NOT called by the Garbage collector
        if (Handle != IntPtr.Zero)
            //after this call the handle will not be use, so no need to set it to zero
            FreeHandle(Handle);
    }
}
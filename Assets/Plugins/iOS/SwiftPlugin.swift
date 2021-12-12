@objc
public class SwiftPlugin: NSObject
{
    static var count: Int = 0;
    
    @objc
    public static func callPlugin() -> String
    {
        count += 1;
        ObjcBridge.sendMessage("count is " + String(count));
        return "Hello, I'm swift.";
    }
}

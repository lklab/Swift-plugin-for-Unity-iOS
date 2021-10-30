#import "objc_bridge.h"
#import "UnityFramework/UnityFramework-Swift.h"

extern "C"
{
    const char* CallPluginIOS()
    {
        NSString *ret = [SwiftPlugin callPlugin];
        const char *nsStringUtf8 = [ret UTF8String];
        char* cString = (char*)malloc(strlen(nsStringUtf8) + 1);
        strcpy(cString, nsStringUtf8);
        return cString;
    }

    void unitySendMessage(const char* message)
    {
        UnitySendMessage("NativeInterface", "CallUnity", message);
    }
}

@implementation ObjcBridge
    
+ (void) sendMessage: (NSString*)message
{
    unitySendMessage([message UTF8String]);
}

@end

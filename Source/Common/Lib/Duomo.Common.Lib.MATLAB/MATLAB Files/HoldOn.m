function HoldOn(graphicsHandle, toggle)
%UNTITLED8 Summary of this function goes here
%   Detailed explanation goes here

if(toggle)
    hold('on', graphicsHandle);
else
    hold('off', graphicsHandle);
end

end


※Check format of configuration file 

Example:

[EqConst]

EC=[ECID=91, Name=EnableSpooling, Type=Boolean, Value=FALSE]

[Vids]

Vid=[ID=100, Name=PortID, Type=U4, Class=DV]

[Events]

Event=[ID=1000, Name=ReadyToLoad, Enable=True]

[Reports]

Report=[ID=1, Name=PortInfo, Vids=[100]];PortID 

[ReportLinks]

ReportLink=[Event=5000, Reports=[1000,1]]

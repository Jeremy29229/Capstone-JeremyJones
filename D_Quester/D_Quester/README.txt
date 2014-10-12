D_Quester V1.0.1

QuestObject is the base class for handling a quest node. It holds anything a quest giver or quest node handler might need to handle what happens before during and after a quest node is started. It contains rewarders, objectives, its current state, and methods to advance its state and perform certain logic when a state is reached.

Quest rewarders are classes that can be listened to by Rewardable classes to remotely update the state or quantity of something.
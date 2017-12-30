Extremely simple console program to send and receive udp

Launch with:
NetCmd "Remote receiver Ip address" "Remote receiver Port" "Port to open on localhost" [Number Of Times the message will be repeated] [Sleep milliseconds between each message replication]

Examples:

NetCmd 192.168.0.123 1000 90 --> Will send messages to 192.168.0.123 on port 1000. Omitting number of replications and sleep between replication, defaults to 1 and 0 respectively.

NetCmd 192.168.0.123 1000 90 3 1000 --> Will send messages to 192.168.0.123 on port 1000; will replicate 3 times each message, sleeping 1 second between each replication.

Once launched, it will prompt without any message: just write down what you want to send and press enter.
You will see incoming messages in the form:

in <-- incoming message

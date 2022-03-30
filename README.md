# Sports Betting Infrastructure & Best Practices

POC -> You are in here -> MVP -> MMP


Technology Stack

![TechStack drawio](https://user-images.githubusercontent.com/1757659/160717740-f073922c-aee8-471f-bbb5-c92efc146272.png)


The infrastructure is consists of three parts:


  ![BetData drawio](https://user-images.githubusercontent.com/1757659/160717840-950b3f9d-92d1-4df4-8ec1-1de77bc79402.png)


  
  ![BetBroker drawio](https://user-images.githubusercontent.com/1757659/160717568-d1e08a3c-6f6d-49da-9ef1-8ee4294d9713.png)



![BetNotification drawio](https://user-images.githubusercontent.com/1757659/160717890-e3d15ae6-e6e8-4976-a0ef-48d96db974bb.png)


1- Install Docker Desktop or Rancher Desktop

2- 'docker-compose up' in src folder

3- you can monitor the system via /log/log*.txt

4- Endpoints:

  - website: http://localhost:80
  - rabbitmq ui: http://localhost:15672/
  - redis ui (redisinsight): http://localhost:8001
  
5- Storages:   

   - sqlserver: Server=localhost;Database=Bet;Trusted_Connection=false;User ID=sa;Password=^!oguz1234O  
   - redis: redis:6379
   - rabbitmq: amqp://guest:guest@rabbitmq:5672


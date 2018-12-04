# 基于microsoft/dotnet:2.1-sdk or dotnet:latest 默认latest 来创建我们的镜像
FROM microsoft/dotnet

#指定docker的工作目录为 esoftor-master文件夹,也就是容器的默认的文件夹
WORKDIR /usr/local/src/esoftor-api-master
#WORKDIR /esoftor-api-master

# docker对外暴露的端口
EXPOSE 60000

# 将当前文件夹下的所有文件全部复制到工作目录(如果是在docker进行持续性继承,该行注释掉),如果单纯的只是迁移到docker,这里需要取消注释
COPY . /usr/local/src/esoftor-api-master

#CMD dotnet ESoftor.WebApi.dll 这里自行百度 CMD和ENTRYPOINT的区别
# ENTRYPOINT ["dotnet", "ESoftor.WebApi.dll"]
CMD dotnet ESoftor.WebApi.dll

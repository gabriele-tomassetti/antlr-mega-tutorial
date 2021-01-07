# ANTLR4 Tutorial - Java Project

If you need help to setup everything you can read the [Java Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#java-setup)

The commands you need to know:
```
# this README assumes that you have installed Gradle in your system
# to generate the JetBrains IntelliJ IDEA project
./gradlew idea
# you can then use standard interface of IntelliJ IDEA to build and run the program
# alternatively, if you just want to use gradle
# to generate the ANTLR4 Parser
./gradlew generateGrammarSource
# to compile the program
./gradlew compileJava
# to create a JAR with all dependencies
./gradlew fatJar
# ... and then run the program
java -jar .\build\libs\markup-example-gradle-all.jar
# to run the tests
./gradlew test
```
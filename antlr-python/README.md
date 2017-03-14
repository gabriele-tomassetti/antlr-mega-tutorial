# ANTLR4 Tutorial - Python Project

If you need help to setup everything you can read the [Python Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#python-setup)

The commands you need to know:
```
// one way to install the ANTLR4 runtime, you can choose any of your favorite alternatives
// including downloading from [PyPi](https://pypi.python.org/pypi/antlr4-python3-runtime/)
sudo -H pip3 install antlr4-python3-runtime
// to generate parser and lexer
antlr4 -Dlanguage=Python3 Chat.g4
// to execute the program
// there will be nothing on stdout: read output.html to see the results
python3 antlr.py input.txt
// to run the tests
python3 -m unittest discover -s . -p ChatTests.py
```
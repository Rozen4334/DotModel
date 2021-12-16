# 🗃 DotConfig
An improved way to manage and use configuration files in your applications!

## ⚠ This project is still completely work in progress. Here is the general idea:

With config files often come an excessive amount of uncommented entries, 
with no idea what kind of value should reside there. DotConfig wants this changed!

## 🏁 Goals for this project:

#### 👍 Config files should look like this:

```markdown
*Your username
- string Name: Rozen

*Your discriminator
- short Discriminator: 1001

*Your favorite servers
- list(ulong) Servers
  - 2101010101010101010
  - 6942000000000000000
  - 1844293183231321312

*Custom commands with unique responses
- dictionary(string, string) Commands
  - [Hello] Hi there
  - [Im cool!] Yea you are!
```
----
#### 😝 And not like this:

```json

{
   "Name": "Rozen",
   "Discriminator": 1001,
   "Servers": [
      88101010101010101,
      6942000000000000000,
      88442931832313
   ],
   "Commands": {
   [
      "Hello",
      "Hi there"
   ],
   [
      "Im cool!",
      "Yea you are!"
   ]}
}
```

## 💭 Issues to overcome:

### 📜 How will we work with new lines?

Lines will be uniquely defined by the '-', 
if you want to define your string with multiple lines, 
adding a line beneath the previous one will automatically use a newline to split & append the different lines.

### 📂 What about dictionary entries, will it still work if I add `[ ]` somewhere else, or even inside the dictionary key? 

Yes. For every ` [ ` in the key, a value will be incremented to account for this. 
The value will decrement when it encounters a ` ] ` until 0. 
If it decrements down to 0, the value will be disposed and every new ` [ ` or ` ] ` will be ignored (as it enters the value of said key).

### 📂 Lists inside dictionaries, or other classes?

```markdown
*A unique format to account for
- dictionary(int, class) Students
  - [1] 
    *A student class, which can also be commented into.
    - model Student

      *A name:
      - string Name: Frank

      *A student number:
      - int StudentNum: 101010101
  - [2]
    - model Student

      *Another name:
      - string Name: Paul

      *A different student number:
      - int StudentNum: 2121212121
```

In this example different comments exist for different entries, which appeals to the improved readability for users. 
This can be done by adding attributes to pre-existing types initialized into this dictionary. An example would be:

```cs
[DotProperty("", "A unique format to account for")]
public Dictionary<int, Student> Students = new() 
   { 
     { 
        1, 
        new Student() 
        { 
           [Description("A name:")] Name: Frank, 
           [Description("A student number:")] StudentNum: 101010101 } 
        }, 
     .. 
   }
```

After the dictionary has been discovered, it will first look for the type set in TKey, 
then in TValue, and evaluate the entrypoint based off of that. 
A key **Can** hold a type, but its not adviced.

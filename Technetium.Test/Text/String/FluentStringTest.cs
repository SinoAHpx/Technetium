using System;
using Technetium.Text;

namespace Technetium.Test.Text.String;

public class FluentStringTest
{
    
    [Test]
    public void TestNullOrEmpty()
    {
        string? str = null;
        Assert.IsTrue(str.IsNullOrEmpty());

        str = "";
        Assert.IsTrue(str.IsNullOrEmpty());

        str = "Hello, World!";
        Assert.IsFalse(str.IsNullOrEmpty());
    }

    [Test]
    public void TestIsInt32()
    {
        Assert.IsTrue("123".IsInt32());
        Assert.IsFalse("123.45".IsInt32());
        Assert.IsFalse("abc".IsInt32());
        Assert.IsFalse("".IsInt32());
        Assert.IsFalse(((string?)null).IsInt32());
    }

    [Test]
    public void TestIsInt64()
    {
        Assert.IsTrue("9223372036854775807".IsInt64());
        Assert.IsFalse("9223372036854775808".IsInt64());
        Assert.IsFalse("abc".IsInt64());
        Assert.IsFalse("".IsInt64());
        Assert.IsFalse(((string?)null).IsInt64());
    }

    [Test]
    public void TestIsDouble()
    {
        Assert.IsTrue("123.45".IsDouble());
        Assert.IsTrue("123".IsDouble());
        Assert.IsFalse("abc".IsDouble());
        Assert.IsFalse("".IsDouble());
        Assert.IsFalse(((string?)null).IsDouble());
    }

    [Test]
    public void TestIsFloat()
    {
        Assert.IsTrue("123.45".IsFloat());
        Assert.IsTrue("123".IsFloat());
        Assert.IsFalse("abc".IsFloat());
        Assert.IsFalse("".IsFloat());
        Assert.IsFalse(((string?)null).IsFloat());
    }

    [Test]
    public void TestIsDecimal()
    {
        Assert.IsTrue("123.45".IsDecimal());
        Assert.IsTrue("123".IsDecimal());
        Assert.IsFalse("abc".IsDecimal());
        Assert.IsFalse("".IsDecimal());
        Assert.IsFalse(((string?)null).IsDecimal());
    }

    [Test]
    public void TestToInt32()
    {
        Assert.That("123".ToInt32(), Is.EqualTo(123));
        Assert.Throws<FormatException>(() => "abc".ToInt32());
    }

    [Test]
    public void TestToInt64()
    {
        Assert.That("9223372036854775807".ToInt64(), Is.EqualTo(9223372036854775807));
        Assert.Throws<FormatException>(() => "abc".ToInt64());
    }

    [Test]
    public void TestToBool()
    {
        Assert.IsTrue("true".ToBool());
        Assert.IsFalse("false".ToBool());
        Assert.Throws<FormatException>(() => "abc".ToBool());
    }

    [Test]
    public void TestCombinePath()
    {
        Assert.That("path".CombinePath("to", "file"), Is.EqualTo(Path.Combine("path", "to", "file")));
    }

    [Test]
    public void TestJoinToString()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.JoinToString(","), Is.EqualTo("1,2,3"));
    }

    [Test]
    public void TestJoinToStringWithSelector()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.JoinToString(",", x => (x * 2).ToString()), Is.EqualTo("2,4,6"));
    }

    [Test]
    public void TestEmpty()
    {
        Assert.That("Hello World".Empty(" "), Is.EqualTo("HelloWorld"));
    }

    [Test]
    public void TestSubstringBetween()
    {
        Assert.That("Hello World!".SubstringBetween("Hello ", "!"), Is.EqualTo("World"));
        Assert.That("FooBar".SubstringBetween("a", "b"), Is.Null);
    }
    
    [Test]
    public void TestSubstringAfter()
    {
        Assert.That("Hello World!".SubstringAfter("Hello "), Is.EqualTo("World!"));
        Assert.That("Hello World".SubstringAfter("Foo"), Is.Null);
    }

    [Test]
    public void TestInsertAfter()
    {
        Assert.That("Hello World!".InsertAfter("Hello", ","), Is.EqualTo("Hello, World!"));
        Assert.That("Hello World!".InsertAfter("o", ","), Is.EqualTo("Hello, World!"));
        Assert.That("Hello World!".InsertAfterEach("o", ","), Is.EqualTo("Hello, Wo,rld!"));
        Assert.That("FooBar".InsertAfter("1","awd"), Is.Null);
    }

    [Test]
    public void TestInsertBefore()
    {
        Assert.That("Hello World!".InsertBefore(" World", ","), Is.EqualTo("Hello, World!"));
        Assert.That("Hello World!".InsertBeforeEach("o", ","), Is.EqualTo("Hell,o W,orld!"));
        Assert.That("FooBar".InsertBefore("Awd", "awd"), Is.Null);
    }
}

using System;
using Xunit;
using FluentAssertions;

namespace ClassStructDifferenceTests
{

    public class Tests
    {
        [Fact]
        public void OneObjectReferenceEqualsDifference()
        {
            MyClass myClass =new MyClass();
            ReferenceEquals(myClass, myClass).Should().BeTrue();

            MyStruct myStruct=new MyStruct();
            ReferenceEquals(myStruct, myStruct).Should().BeFalse();
        }

        [Fact]
        public void CopyingValueDifference()
        {
            MyClass myClass = new MyClass();
            MyClass myClass1 = myClass;
            myClass1.Status = "New Status";
            myClass.Status.Should().Be(myClass1.Status);

            MyStruct myStruct = new MyStruct();
            MyStruct myStruct1 = myStruct;
            myStruct1.Status = "New Status";
            myStruct.Status.Should().NotBe(myStruct1.Status);
        }

        [Fact]
        public void MethodArgumentsDifferemnce()
        {
            Action<MyClass> methodClass = arg => arg.Status = "New Status";
            MyClass myClass = new MyClass();
            methodClass.Invoke(myClass);
            myClass.Status.Should().Be("New Status");

            MyStruct myStruct = new MyStruct();
            Action<MyStruct> methodStruct = arg => arg.Status = "New Status";
            methodStruct.Invoke(myStruct);
            myStruct.Status.Should().NotBe("New Status");
        }

        [Fact]
        public void DefaultValueDifference()
        {
            MyClass myClass=default(MyClass);
            myClass.Should().BeNull();           

            MyStruct myStruct = default(MyStruct);
            myStruct.Should().NotBeNull();
        }

        [Fact]
        public void ValueTypeInheritanceDifference()
        {
            typeof(ValueType).IsAssignableFrom(typeof(MyClass)).Should().BeFalse();

            typeof(ValueType).IsAssignableFrom(typeof(MyStruct)).Should().BeTrue();
        }

        [Fact]
        public void TypeofPropertyDifference()
        {
            typeof(MyClass).IsClass.Should().BeTrue();
            typeof(MyClass).IsValueType.Should().BeFalse();
       
            typeof(MyStruct).IsClass.Should().BeFalse();
            typeof(MyStruct).IsValueType.Should().BeTrue();
        }
    }
}

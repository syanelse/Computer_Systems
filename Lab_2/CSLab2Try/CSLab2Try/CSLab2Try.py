def Multiply(multiplicand, multiplier):
    product = 0;
    multiplicand <<= multiplier.bit_length();
    for i in range(multiplier.bit_length()):
        print("Множник: {0:b}".format(multiplier))
        if(multiplier % 2 != 0):
            print("Останній біт множника -- 1, додаємо множиме до добутку.")
            product += multiplicand;
        multiplier >>= 1;
        product >>= 1;
        print("Біт {0:2}".format(i), ", значення добутку: {0:b}".format(product), ", зсуваємо множник і добуток праворуч.", sep='');
    if multiplier < 0:
        product -= multiplicand;
        print("Значення множника менше 0, треба зробити поправку. Добуток: ", product);
    print("Остаточний результат: ", product);
    return product;

def Divide(divident, divider):
    quotient = 0;
    remainder = divident;
    divider <<= divident.bit_length();
    for i in range(divident.bit_length() + 1):
        print("Залишок: {0:b}".format(remainder), ", дільник: {0:b}".format(divider), ", частка: {0:b}".format(quotient), sep = '')
        quotient <<= 1;
        print("Зсуваємо частку ліворуч. Значення частки: {0:b}".format(quotient));
        if (remainder - divider >= 0):
            quotient += 1;
            remainder -= divider;
            print("Залишок більше, ніж дільник. Віднімаємо дільник, збільшуємо частку. Залишок: {0:b}".format(remainder), ", дільник: {0:b}".format(divider), ", частка: {0:b}".format(quotient), sep = '' );
        divider >>= 1;
        print("Зсуваємо дільник праворуч.");
    print("Кінець алгоритму. Значення частки: ", quotient, ", остача: ", remainder)
    return quotient;

Divide(67, 9)
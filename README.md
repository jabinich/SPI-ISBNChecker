# SPI-ISBNChecker
 Check the validation of a ISBN-10

An International Standard Book Number (ISBN) consists of four parts (if it is a 10-digit ISBN): Group, Publisher, Title and Check digit, or five parts (for a 13-digit ISBN): EAN, Group, Publisher, Title and Check digit.

ISBN-10 check digit calculation:

Each of the first nine digits of the 10-digit ISBN – excluding the check digit itself – is multiplied by its (integer) weight, descending from 10 to 2, and the sum of these nine products found. The value of the check digit is simply the one number between 0 and 10 which, when added to this sum, means the total is a multiple of 11.

For example, the check digit for an ISBN-10 of 0-306-40615-? is calculated as follows:

s = (0x10)+(3x9)+(0x8)+(6x7)+(4x6)+(0x5)+(6x4)+(1x3)+(5x2) = 130

Adding 2 to 130 gives a multiple of 11 (because 132 = 12×11) – this is the only number between 0 and 10 which does so. Therefore, the check digit has to be 2, and the complete sequence is ISBN 0-306-40615-2. If the value of x10 required to satisfy this condition is 10, then an 'X' should be used.


Check the validation of a ISBN-13

The calculation of an ISBN-13 check digit begins with the first twelve digits of the 13-digit ISBN (thus excluding the check digit itself). Each digit, from left to right, is alternately multiplied by 1 or 3, then those products are summed modulo 10 to give a value ranging from 0 to 9. Subtracted from 10, that leaves a result from 1 to 10. A zero replaces a ten, so, in all cases, a single check digit results.

For example, the ISBN-13 check digit of 978-0-306-40615-? is calculated as follows:

s = (9x1)+(7x3)+(8x1)+(9x3)+(8x1)+(6x3)+(1x1)+(8x3)+(1x1)+(7x3)+(2x1)+(8x3) = 164

Adding 6 to 164 gives a multiple of 10 (170=17x10). Thus, the check digit is 6, and the complete sequence is ISBN 978-986-181-728-6.
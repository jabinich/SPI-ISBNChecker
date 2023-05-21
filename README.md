# SPI-ISBNChecker
 Check the correctness of a ISBN-10

An International Standard Book Number (ISBN) consists of four parts (if it is a 10-digit ISBN): Group, Publisher, Title and Check digit, or five parts (for a 13-digit ISBN): EAN, Group, Publisher, Title and Check digit.

ISBN-10 check digit calculation:

Each of the first nine digits of the 10-digit ISBN – excluding the check digit itself – is multiplied by its (integer) weight, descending from 10 to 2, and the sum of these nine products found. The value of the check digit is simply the one number between 0 and 10 which, when added to this sum, means the total is a multiple of 11.

For example, the check digit for an ISBN-10 of 0-306-40615-? is calculated as follows:

s = (0x10)+(3x9)+(0x8)+(6x7)+(4x6)+(0x5)+(6x4)+(1x3)+(5x2) = 130

Adding 2 to 130 gives a multiple of 11 (because 132 = 12×11) – this is the only number between 0 and 10 which does so. Therefore, the check digit has to be 2, and the complete sequence is ISBN 0-306-40615-2. If the value of x10 required to satisfy this condition is 10, then an 'X' should be used.

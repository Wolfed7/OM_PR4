import matplotlib.pyplot as plt
import numpy as np


def f(x1, x2):
    C = [ 2, 1,  7,  2,  8,  4 ]
    a = [ 5, 2, -9,  0, -3, -3 ]
    b = [ 4, 0, -6, -3,  7,  3 ]

    sum = 0;
    for i in range(0, 6):
       sum += C[i] / ( 1 + (x1 - a[i]) * (x1 - a[i]) + (x2 - b[i]) * (x2 - b[i]) )
    return sum


x1_min = -10.0
x1_max = 10.0
x2_min = -10.0
x2_max = 10.0

x1, x2 = np.meshgrid(np.arange(x1_min,x1_max, 0.01), np.arange(x2_min,x2_max, 0.01))

y = f(x1,x2)

plt.imshow(y,extent=[x1_min,x1_max,x2_min,x2_max], cmap='Greys', origin='lower')

plt.colorbar()


plt.show()

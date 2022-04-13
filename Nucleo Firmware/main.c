
/* Linear Region estimator

Using linear regression, determine slope and R2 value of various sections of data.
The region of data with the R2 value closes to 1 is the slope to be used for further calculations.

By: Vanessa McGaw

*/

#include <math.h>
#include <stdio.h>

#define SECTIONS 10// divides data into __ sections

int main()
{
    double results[2];
    double slope;
    double thermCond;
    double q = (0.01*0.01*1)/0.066; // q = I*I*R/L
    
    int a;
   	double x[] = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,\
                22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,\
                40,41,42,43,44,45,46,47,48,49,50};

        
    double y[] = {1,3,3,2,1,1,2,3,2,3,2,4,6,5,7,8,10,9,12,10,10,12,12,12,\
                13,15,16,17,19,18,19,22,23,22,25,26,26,28,29,29,32,31,\
                31,30,30,31,30,31,32,33};
   
   
  int numPoints = sizeof x / sizeof *x; //number of data points collected
 
   int sectSize = numPoints / SECTIONS; // number of data points in each section
   
   
	slope_linear_region(x,y,numPoints, sectSize, results);
	
	slope = results[1];
	
	double calcThermCond(double, double );//function declaration
	thermCond = calcThermCond(q,slope);
	
    
   printf (" The R2 value is: %f", results[0]);  
   printf ("\n The slope is: %f", results[1]); 
   printf ("\n\n The Thermal Conductivity is: %f", thermCond); 

    return 0;
}


void slope_linear_region(double logTime[], double temp[], int numPoints, int sectSize, double* results)
{
   double timeMatrix[SECTIONS][sectSize]; 
   double tempMatrix[SECTIONS][sectSize]; 
   
   double maxR2, linSlope, tempR2;
   
   splitArray(logTime, sectSize, SECTIONS, timeMatrix);
   splitArray(temp, sectSize, SECTIONS, tempMatrix);
   
   
   // initialize two arrays to store slopes and R2 values for each 10% increment section of data
   double slopes[SECTIONS];
   double r2Values[SECTIONS];

   double linRegResults[2]; 
   
   int i;
   for(i=0; i<SECTIONS; i++)
   {
       lin_Regression(timeMatrix[i], tempMatrix[i],sectSize, linRegResults); // may have to use &timeMatrix[i] if this doesnt work
       slopes[i] = linRegResults[0];
       r2Values[i] = linRegResults[1];
   }
   
   maxR2 =1;
   
   do
   {
   		tempR2 = maxR2;
   		maxR2 = r2Values[0];
   		linSlope = slopes[0];
   		
   		for(i=0; i<SECTIONS; i++)
   		{
       		if(r2Values[i] > maxR2 && r2Values[i] < tempR2)
        		{
           		 	maxR2 = r2Values[i];
            		linSlope = slopes[i];
        		}
   		}
   }
   while(linSlope<0);//repeat until slope in non-negative
      
   results[0] = maxR2;
   results[1] = linSlope;
   
}//end of slope_lin_Slope function


/* splitArray is a function that allows you to input an array, and matrix dimensions desired to generate
    a matrix from the given array.
    ex: array = {1, 2, 3, 4, 5, 6, 7, 8}
        size = 4
        rows = sizeOf array / size = 8/2 = 4

        Matrix = {
            {1, 2, 3, 4},
            {5, 6, 7, 8}
                  }
*/
void splitArray(double* array[], int size, int rows, double* result[rows][size])
{

    int k=0;
    int i, j;

    for(i=0; i<rows; i++)
    {
        for(j=0; j<size; j++)
        {
            result[i][j] = array[k];
                     
            k++;
        }
    }
}// end of splitArray Function




/* lin_Regression function takes the x and y data values and dertermines the a and b coefficients for y=a+bx linear regression
    where b = slope, along with R2 value.

    Returns results [2] = [b, R2]
*/
void lin_Regression(double x[], double y[], int n, double* result)
{
    double a, b, R2, ypred, ydiff, ydiff2, var, var2, yAvg;

    double sumx = 0;
    double sumy = 0;
    double sumx2 = 0;
    double sumy2 =0;
    double sumxy = 0;
    double sumyDiff2 = 0;
    double totalVar = 0;
	int i;
	
    for(i=0; i<n ; i++)
   		sumx = sumx + x[i];	
	    
    for(i=0; i<n; i++)
        sumy = sumy + y[i];

    for(i=0; i<n ; i++)
        sumx2 = sumx2 + x[i]*x[i];
    
    for(i=0; i<n ; i++)
        sumy2 = sumy2 + y[i]*y[i];
    
    for(i=0; i<n ; i++)
        sumxy = sumxy + x[i]*y[i];

    a = ((sumy*sumx2) - (sumx*sumxy)) / ((n*sumx2) - (sumx*sumx)); // y-intercept

    b = ((n*sumxy) - (sumx*sumy)) / ((n*sumx2) - (sumx*sumx)); // slope 

    for(i=0; i<n; i++)
    {
        ypred = a + b*x[i]; 
        
        ydiff = y[i] - ypred;

        ydiff2 = ydiff*ydiff;
          
        yAvg = sumy / n;
    
        var = y[i] - yAvg;

        var2 = var*var;

        sumyDiff2 = sumyDiff2 + ydiff2;
        
        totalVar = totalVar + var2;
      }

    R2 = 1 - (sumyDiff2/totalVar);
    
    result[0] = b;
    result[1] = R2;
    
}//end of lin_regression funtcion

double calcThermCond(double heatPower, double slope)
{
	return heatPower / (4*M_PI*slope);
}






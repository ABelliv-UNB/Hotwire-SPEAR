/*
 * i2c-lcd.c
 *
 *  Created on: Mar 9, 2022
 *      Author: Vanessa MCGaw
 *      Some code from: https://controllerstech.com/i2c-lcd-in-stm32/
 *      			    https://www.lcd-module.de/support/application-note.html
 *
 */
#define LCD_SLAVE_ADDRESS 0x7A;

void lcd_send_data(char data)
{
	//hi2x1 - pointer to I2C_HandleTypeDef structure that contains configuration information
	//LCD_SLAVE_ADDRESS - defined slave address for LCD from datasheet (SA0 = 3.3V)
	//data - 8 bit data
	// 8 - number of bits of data to be sent
	//100 - timeout duration
	HAL_I2C_Master_Transmit(&hi2c1, LCD_SLAVE_ADDRESS, (uint8_t*) data, 8, 100);

}

void lcd_init(void)
{
	HAL_Delay(1);
	lcd_send_cmd(0x3A);	//8-Bit data length extension Bit RE=1; REV=0
	HAL_Delay(1);
	lcd_send_cmd(0x09);	//4 line display
	HAL_Delay(1);
	lcd_send_cmd(0x06);	//Bottom view
	HAL_Delay(1);
	lcd_send_cmd(0x1E);	//Bias setting BS1=1
	HAL_Delay(1);
	lcd_send_cmd(0x39);	//8-Bit data length extension Bit RE=0; IS=1
	HAL_Delay(1);
	lcd_send_cmd(0x1B);	//BS0=1 -> Bias=1/6
	HAL_Delay(1);
	lcd_send_cmd(0x6E); //Devider on and set value
	HAL_Delay(1);
	lcd_send_cmd(0x57); //Booster on and set contrast (BB1=C5, DB0=C4)
	HAL_Delay(1);
	lcd_send_cmd(0x72); //Set contrast (DB3-DB0=C3-C0)
	HAL_Delay(1);
	lcd_send_cmd(0x38); //8-Bit data length extension Bit RE=0; IS=0
}

void lcd_send_string(char *str)
{
	while(*str)
	{
		lcd_send_data(*str)
		*str++;
	}
}

void lcd_clear_display(void) // will need to test to verify functionality.
{
	for(int i=0; i<80; i++)// display is 4x20
		lcd_send_data(' '); // put blank in each location of the display

}

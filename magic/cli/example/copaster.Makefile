NAME ?= Egor

in-output:
	echo "Hello, $(NAME). Here are the files in output"
	ls

in-caller:
	echo "Hello, $(NAME). Here are the files in caller"
	ls
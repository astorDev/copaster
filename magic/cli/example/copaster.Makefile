NAME ?= Egor

in-output:
	echo "Hello, $(NAME). Here's files in output"
	ls

in-caller:
	echo "Hello, $(NAME). Here's files in caller"
	ls
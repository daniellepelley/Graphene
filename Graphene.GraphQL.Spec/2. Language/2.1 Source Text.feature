
2.1Source Text

SourceCharacter
/[\u0009\u000A\u000D\u0020-\uFFFF]/
GraphQL documents are expressed as a sequence of Unicode characters. However, with few exceptions, most of GraphQL is expressed only in the original non‐control ASCII range so as to be as widely compatible with as many existing tools, languages, and serialization formats as possible and avoid display issues in text editors and source control.

2.1.1Unicode

UnicodeBOM
Byte Order Mark (U+FEFF)
Non‐ASCII Unicode characters may freely appear within StringValue and Comment portions of GraphQL.

The “Byte Order Mark” is a special Unicode character which may appear at the beginning of a file containing Unicode which programs may use to determine the fact that the text stream is Unicode, what endianness the text stream is in, and which of several Unicode encodings to interpret.

Scenario: Non‐ASCII Unicode characters may freely appear within StringValue and Comment portions of GraphQL
	Given I have a GraphQL document wih
	When I press add
	Then the result should be 120 on the screen


# 2.1.2White Space

# WhiteSpace
#Horizontal Tab (U+0009)
#Space (U+0020)
#White space is used to improve legibility of source text and act as separation between tokens, and any amount of white space may appear before or after any token. White space between tokens is not significant to the semantic meaning of a GraphQL query document, however white space characters may appear within a String or Comment token.

#GraphQL intentionally does not consider Unicode “Zs” category characters as white‐space, avoiding misinterpretation by text editors and source control tools.



#2.1.3Line Terminators

#LineTerminator
#New Line (U+000A)
#Carriage Return (U+000D)New Line (U+000A)
#Carriage Return (U+000D)New Line (U+000A)
#Like white space, line terminators are used to improve the legibility of source text, any amount may appear before or after any other token and have no significance to the semantic meaning of a GraphQL query document. Line terminators are not found within any other token.

#Any error reporting which provide the line number in the source of the offending syntax should use the preceding amount of LineTerminator to produce the line number.
#2.1.4Comments

#Comment
#CommentCharlistopt
#CommentChar
#SourceCharacterLineTerminator
#GraphQL source documents may contain single‐line comments, starting with the # marker.

#A comment can contain any Unicode code point except LineTerminator so a comment always consists of all code points starting with the # character up to but not including the line terminator.

#Comments behave like white space and may appear after any token, or before a line terminator, and have no significance to the semantic meaning of a GraphQL query document.

#2.1.5Insignificant Commas

#Comma

#Similar to white space and line terminators, commas (,) are used to improve the legibility of source text and separate lexical tokens but are otherwise syntactically and semantically insignificant within GraphQL query documents.

#Non‐significant comma characters ensure that the absence or presence of a comma does not meaningfully alter the interpreted syntax of the document, as this can be a common user‐error in other languages. It also allows for the stylistic use of either trailing commas or line‐terminators as list delimiters which are both often desired for legibility and maintainability of source code.

#2.1.6Lexical Tokens

#Token
#Punctuator
#Name
#IntValue
#FloatValue
#StringValue
#A GraphQL document is comprised of several kinds of indivisible lexical tokens defined here in a lexical grammar by patterns of source Unicode characters.

#Tokens are later used as terminal symbols in a GraphQL query document syntactic grammars.

#2.1.7Ignored Tokens

#Ignored
#UnicodeBOM
#WhiteSpace
#LineTerminator
#Comment
#Comma
#Before and after every lexical token may be any amount of ignored tokens including WhiteSpace and Comment. No ignored regions of a source document are significant, however ignored source characters may appear within a lexical token in a significant way, for example a String may contain white space characters.

#No characters are ignored while parsing a given token, as an example no white space characters are permitted between the characters defining a FloatValue.

#2.1.8Punctuators

#Punctuator
#!	$	(	)	...	:	=	@	[	]	{	}
#GraphQL documents include punctuation in order to describe structure. GraphQL is a data description language and not a programming language, therefore GraphQL lacks the punctuation often used to describe mathematical expressions.

#2.1.9Names

#Name
#/[_A-Za-z][_0-9A-Za-z]*/
#GraphQL query documents are full of named things: operations, fields, arguments, directives, fragments, and variables. All names must follow the same grammatical form.

#Names in GraphQL are case‐sensitive. That is to say name, Name, and NAME all refer to different names. Underscores are significant, which means other_name and othername are two different names.

#Names in GraphQL are limited to this ASCII subset of possible characters to support interoperation with as many other systems as possible.